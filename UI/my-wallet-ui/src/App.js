import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap';
import { useEffect, useState } from 'react';

function App() {

  const baseUrl = 'https://localhost:7240/Persons'

  // states --------------------------------------------------------------------------------
  const [data, setData] = useState([]);
  const [selectedPerson, setSelectedPerson] = useState({
    id: '',
    name: '',
    email: ''
  })
  const [modalIncludePerson, setModalIncludePerson] = useState(false);
  const [modalUpdatePerson, setModalUpdatePerson] = useState(false);
  const [successMessage, setSuccessMessage] = useState('');

  // --------------------------------------------------------------------------------
  const getPersons = async()=> 
  {
    await axios.get(baseUrl)
              .then( response => {
                setData(response.data.rows);
              })
              .catch(error => {
                console.log(error);
              });
  }

  const handleChange = e => {
    const {name, value} = e.target;
    setSelectedPerson({
      ...selectedPerson,
      [name]: value
    });
  }

  const openCloseModalIncludePerson = () => {
    setModalIncludePerson(!modalIncludePerson);
  }

  const openCloseModalUpdatePerson = () => {
    setModalUpdatePerson(!modalUpdatePerson);
  }

  const updateOrDeletePerson = (person, option) => {
    setSelectedPerson(person);
    (option === 'update') &&
      openCloseModalUpdatePerson();
  }

  const postPerson = async() => {
    delete selectedPerson.id;
    await axios.post(baseUrl, selectedPerson)
                .then( response => {
                  setData(data.concat(
                    response.data
                  ));
                  openCloseModalIncludePerson();
                  setSuccessMessage("Pessoa cadastrada com sucesso!");
                  setTimeout(() => setSuccessMessage(""), 3000); // limpa após 3s
                })
                .catch(error => {
                  console.log(error);
                })
  }

  const patchPerson = async() => {
    await axios.patch(baseUrl + "/" + selectedPerson.id, selectedPerson)
                .then(response => {
                  var responseData = response.data;
                  var auxData = data;
                  
                  auxData.map(person => {
                    if(person.id === selectedPerson.id) {
                      person.name = responseData.name;
                      person.email = responseData.email;
                    }
                  });

                  openCloseModalUpdatePerson();
                  setSuccessMessage("Pessoa alterada com sucesso!");
                  setTimeout(() => setSuccessMessage("", 3000));
                })
                .catch(error => {
                  console.log(error);
                })
  }

  useEffect(
    () => {
      getPersons()
    }, []
  );

  return (
    <div className="person-container">
      <header>
        {successMessage && <div className="alert alert-success">{successMessage}</div>}
        <h1>Cadastro de pessoas</h1>
        
      </header>
      <main className='person-container'>
        <button className='btn btn-success' onClick={() => openCloseModalIncludePerson()}>Incluir Nova Pessoa</button>
        <table className='table table-bordered'>
          <thead>
            <tr>
              <th>Id</th>
              <th>Nome</th>
              <th>E-mail</th>
              <th>Operações</th>
            </tr>
          </thead>
          <tbody>
            {
              data.map(person =>(
                <tr key={person.id}>
                  <td>{person.id}</td>
                  <td>{person.name}</td>
                  <td>{person.email}</td>
                  <td>
                    <button className='btn btn-primary' onClick={() => updateOrDeletePerson(person, "update")}>Editar</button> {"    "}
                    <button className='btn btn-danger' onClick={() => updateOrDeletePerson(person, "delete")}>Excluir</button>
                  </td>
                </tr>
              ))
            }
          </tbody>
        </table>
      </main>
      
      <Modal isOpen={modalIncludePerson} >
        <ModalHeader>Incluir Pessoa</ModalHeader>
        <ModalBody>
          <div className='form-group'>
            <label>Nome*:</label>
            <br/>
            <input type='text' className='form-control' name='name' required onChange={handleChange} />
            <br />
            <label>E-mail*:</label>
            <br />
            <input type='text' className='form-control' name='email' required onChange={handleChange} />
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
            <br/>
            <input  type='text' 
                    className='form-control' 
                    name='id' 
                    value={selectedPerson && selectedPerson.id}
                    readOnly
                    onChange={handleChange} />
            <br />
            <label>Nome*:</label>
            <br/>
            <input  type='text' 
                    className='form-control' 
                    name='name' 
                    required 
                    value={selectedPerson && selectedPerson.name}
                    onChange={handleChange} />
            <br />
            <label>E-mail*:</label>
            <br />
            <input  type='text' 
                    className='form-control' 
                    value={selectedPerson && selectedPerson.email}
                    name='email' 
                    required
                    onChange={handleChange} />
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-primary' onClick={() => patchPerson()} >Alterar</button>
          <button className='btn btn-danger' onClick={() => openCloseModalUpdatePerson()}>Cancelar</button>
        </ModalFooter>
      </Modal>
    </div>
  );
}

export default App;
