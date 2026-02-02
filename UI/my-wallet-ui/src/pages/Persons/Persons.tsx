import { useEffect, useState } from "react";
import apiHandler from "../../apiHandler";
import Navbar from "../../components/Navbar";
import { Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import { useToast } from "../../contexts/ToastContext/ToastContext";

export function Persons() {
    const baseUrl = 'https://localhost:7240/Persons'
    const { showToast } = useToast();

    // states --------------------------------------------------------------------------------
    const [data, setData] = useState([]);
    const [updateData, setUpdateData] = useState(true);
    const [selectedPerson, setSelectedPerson] = useState({
        id: null,
        name: null
    })

    const [modalIncludePerson, setModalIncludePerson] = useState(false);
    const [modalUpdatePerson, setModalUpdatePerson] = useState(false);
    const [modalDeletePerson, setModalDeletePerson] = useState(false);

    const [errors, setErrors] = useState<Errors>({});

    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(1);

    const [filterName, setFilterName] = useState("");

    // Types ---------------------------------
    type Errors = {
        Name?: string;
    };

    // --------------------------------------------------------------------------------
    const getPersons = async (pageIndex = currentPage, size = pageSize, filter = filterName) => {
        await apiHandler
            .get(`/Persons?pageIndex=${pageIndex}&pageSize=${pageSize}&name=${filter}`)
            .then(response => {
                setData(response.data.rows);
                setCurrentPage(response.data.currentPage);
                setPageSize(response.data.pageSize);
                setTotalPages((response.data.totalPages > 0) ? response.data.totalPages : 1);
            })
            .catch(error => {
                console.log(error);
            });
    }

    const handleChange = e => {
        const { name, value } = e.target;
        setSelectedPerson({
            ...selectedPerson,
            [name]: value === "" ? "" : value
        });
    }

    const openCloseModalIncludePerson = () => {
        setErrors({});
        setSelectedPerson({
            id: null,
            name: ""
        });
        setModalIncludePerson(!modalIncludePerson);
    }

    const openCloseModalUpdatePerson = () => {
        setErrors({});
        setModalUpdatePerson(!modalUpdatePerson);
    }

    const openCloseModalDeletePerson = () => {
        setModalDeletePerson(!modalDeletePerson);
    }

    const updateOrDeletePerson = (person, option) => {
        setSelectedPerson(person);
        (option === 'update') ?
            openCloseModalUpdatePerson() : openCloseModalDeletePerson();
    }

    // Pagination
    const nextPage = () => {
        if (currentPage < totalPages) {
            getPersons(currentPage + 1, pageSize);
        }
    }

    const previousPage = () => {
        if (currentPage > 1) {
            getPersons(currentPage - 1, pageSize);
        }
    }

    const changePageSize = (e) => {
        const newSize = parseInt(e.target.value);
        setPageSize(newSize);
        getPersons(1, newSize);
    }

    // API requests

    const postPerson = async () => {
        const personToSend = {
            name: selectedPerson.name
        };

        await apiHandler.post(baseUrl, personToSend)
            .then(response => {
                setData(data.concat(
                    response.data
                ));
                setUpdateData(true);

                openCloseModalIncludePerson();
                showToast({
                    message: "Pessoa cadastrada com sucesso!",
                    type: "success",
                });
                setErrors({});
            })
            .catch(error => {
                if (error.response && error.response.status === 400) {
                    setErrors(error.response.data.errors)
                } else {
                    showToast({
                        message: "Erro interno do servidor. Tente novamente mais tarde.",
                        type: "error"
                    })
                }
            });
    }

    const putPerson = async () => {
        await apiHandler.put(baseUrl + "/" + selectedPerson.id, selectedPerson)
            .then(response => {
                var responseData = response.data;

                setData(data.map(person =>
                    person.id === selectedPerson.id
                        ? { ...person, name: responseData.name }
                        : person
                ));
                setUpdateData(true);

                openCloseModalUpdatePerson();
                showToast({
                    message: "Pessoa alterada com sucesso!",
                    type: "success"
                });
                setErrors({});
            })
            .catch(error => {
                if (error.response && error.response.status === 400) {
                    setErrors(error.response.data.errors);
                } else {
                    console.log(error.response);
                }
            })
    }

    const deletePerson = async () => {
        await apiHandler.delete(baseUrl + "/" + (selectedPerson && selectedPerson.id))
            .then(response => {
                var responseData = response.data;
                setData(
                    data.filter(person => person.id !== responseData.id)
                );
                setUpdateData(true);

                openCloseModalDeletePerson();
                showToast({
                    message: "Pessoa excluida com sucesso!",
                    type: "success"
                });
            })
            .catch(error => {
                console.log(error);
            });
    }

    useEffect(
        () => {
            if (updateData) {
                getPersons(currentPage, pageSize, filterName);
                setUpdateData(false);
            }
        }, [updateData]
    );

    return (
        <div className="person-container">
            <Navbar />

            <main className='person-container'>
                <div className='d-flex align-items-center mb-3 gap-2'>

                    <input type='text'
                        className='form-control me-2'
                        placeholder='Filtrar por nome'
                        value={filterName}
                        onChange={(e) => setFilterName(e.target.value)}
                    />
                    <button
                        className="btn btn-primary"
                        onClick={() => getPersons(1, pageSize)}
                    >
                        Buscar
                    </button>
                </div>
                <div className='d-flex justify-content-end mb-3'>
                    <button
                        className='btn btn-success'
                        onClick={openCloseModalIncludePerson}
                    >
                        Incluir Nova Pessoa
                    </button>
                </div>

                <table className='table table-bordered table-container'>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Nome</th>
                            <th className='table-buttons'>Operações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            data.map(person => (
                                <tr key={person.id}>
                                    <td>{person.id}</td>
                                    <td>{person.name}</td>
                                    <td className='table-buttons'>
                                        <button
                                            className='btn btn-primary'
                                            onClick={() => updateOrDeletePerson(person, "update")}
                                        >
                                            Editar
                                        </button> {"    "}
                                        <button
                                            className='btn btn-danger'
                                            onClick={() => updateOrDeletePerson(person, "delete")}
                                        >
                                            Excluir
                                        </button>
                                    </td>
                                </tr>
                            ))
                        }
                    </tbody>
                </table>

                <div className='d-flex justify-content-between align-items-center mt-3'>
                    <div>
                        <button
                            className='btn btn-secondary me-2'
                            onClick={previousPage}
                            disabled={currentPage === 1}
                        >
                            Anterior
                        </button>
                        <button
                            className='btn btn-secondary'
                            onClick={nextPage}
                            disabled={currentPage === totalPages}
                        >
                            Próxima
                        </button>
                    </div>

                    <div>Página {currentPage}  de {totalPages}</div>

                    <div>
                        <label className='me-2'>Itens por página:</label>
                        <select value={pageSize} onChange={changePageSize} className='form-select d-inline w-auto'>
                            <option value={5}>5</option>
                            <option value={10}>10</option>
                            <option value={20}>20</option>
                            <option value={100}>100</option>
                        </select>
                    </div>
                </div>

            </main>

            <Modal isOpen={modalIncludePerson} >
                <ModalHeader>Incluir Pessoa</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label>Nome*:</label>
                        <br />
                        <input type='text'
                            className={`form-control ${errors.Name ? "is-invalid" : ""}`}
                            name='name'
                            required
                            onChange={handleChange} 
                        />
                        { errors.Name && (
                            <div className='invalid-feedback custom-feedback'>
                                {errors.Name[0]}
                            </div>
                        )}
                        <br />
                        <br />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-primary' onClick={() => postPerson()} >Incluir</button>
                    <button className='btn btn-danger' onClick={() => openCloseModalIncludePerson()}>Cancelar</button>
                </ModalFooter>
            </Modal>

            <Modal isOpen={modalUpdatePerson} >
                <ModalHeader>Alterar Pessoa</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label>Id:</label>
                        <br />
                        <input type='text'
                            className='form-control'
                            name='id'
                            value={selectedPerson && selectedPerson.id}
                            readOnly
                            onChange={handleChange} />
                        <br />
                        <label>Nome*:</label>
                        <br />
                        <input type='text'
                            className={`form-control ${errors.Name ? "is-invalid" : ""}`}
                            name='name'
                            required
                            value={selectedPerson && selectedPerson.name}
                            onChange={handleChange} />
                        {errors.Name && (
                            <div className='invalid-feedback custom-feedback'>
                                {errors.Name[0]}
                            </div>
                        )}
                        <br />
                        <br />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-primary' onClick={() => putPerson()} >Alterar</button>
                    <button className='btn btn-danger' onClick={() => openCloseModalUpdatePerson()}>Cancelar</button>
                </ModalFooter>
            </Modal>

            <Modal isOpen={modalDeletePerson}>
                <ModalBody>
                    Deseja realmente excluir este registro: {selectedPerson && selectedPerson.name}?
                </ModalBody>
                <ModalFooter className='w-auto'>
                    <button className='btn btn-danger' onClick={() => deletePerson()}>Sim</button>
                    <button className='btn btn-secondary' onClick={() => openCloseModalDeletePerson()}>Não</button>
                </ModalFooter>
            </Modal>
        </div>
    );
}
