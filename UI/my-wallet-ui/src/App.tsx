import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import { BrowserRouter } from 'react-router-dom';
import { AppRoutes } from './routes/AppRoutes';
import { ToastProvider } from './contexts/ToastContext';

function App() {
  return (
    <BrowserRouter>
      <ToastProvider>
        <AppRoutes />
      </ToastProvider>
    </BrowserRouter>
  )
}

export default App;
