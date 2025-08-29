import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import axios from 'axios';
import { Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap';
import { useEffect, useState } from 'react';

function App() {

  const baseUrl = 'https://localhost:7240/Persons'

  const [data, setData] = useState([]);

  const getPersons = async()=> {
    await axios.get(baseUrl)
    .then( response => {
      setData(response.data.rows);
    })
    .catch(error => {
      console.log(error);
    });
  }

  useEffect(
    () => {
      getPersons()
    }, []
  );

  return (
    <div className="App">
      <br/>
      <h3>Cadastro de pessoas</h3>
      <header className="App-header">
        <button className='btn btn-success'>Incluir Nova Pessoa</button>
      </header>
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
                  <button className='btn btn-primary'>Editar</button> {"    "}
                  <button className='btn btn-danger'>Excluir</button>
                </td>
              </tr>
            ))
          }
        </tbody>
      </table>
    </div>
  );
}

export default App;
