import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import { BrowserRouter } from 'react-router-dom';
import { AppRoutes } from './routes/AppRoutes';
import { ToastListener } from './listeners/ToastListener';
import { ToastProvider } from './contexts/ToastContext/ToastProvider';
import { AuthListener } from './listeners/AuthListener';

function App() {
  return (
    <BrowserRouter>
      <ToastProvider>
        <ToastListener />
        <AuthListener />
        <AppRoutes />
      </ToastProvider>
    </BrowserRouter>
  )
}

export default App;
