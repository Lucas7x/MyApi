import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import { BrowserRouter } from 'react-router-dom';
import { AppRoutes } from './routes/AppRoutes';
import { ToastListener } from './contexts/ToastContext/ToastListener';
import { ToastProvider } from './contexts/ToastContext/ToastProvider';

function App() {
  return (
    <BrowserRouter>
      <ToastProvider>
        <ToastListener />
        <AppRoutes />
      </ToastProvider>
    </BrowserRouter>
  )
}

export default App;
