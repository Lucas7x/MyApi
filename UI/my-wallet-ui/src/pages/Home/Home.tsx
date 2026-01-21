import { useNavigate } from "react-router-dom";
// import Navbar from "../../components/Navbar";
import Navbar from "../../components/Navbar";

export function Home() {
    const navigate = useNavigate();

    return (
        <div>
            <Navbar />
            
            
        </div>
    );
}