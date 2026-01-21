import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Navbar.css";

const Navbar: React.FC = () => {
    const [ clicked, setClicked ] = useState(false);
    const navigate = useNavigate();

    const handleClick = () => {
        setClicked(!clicked);
    };

    const goTo = (path: string) => {
        navigate(path);
        setClicked(false);
    }

    return (
        <nav>
            <div style={{ cursor: "alias" }}
                onClick={ () => goTo("/") }>
            

                <svg width="50"
                    height="40"
                    viewBox="0 0 50 40"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                >
                    <path
                        d="M43 31L31 40H5L7 35L12 31H29L32 35L40 11L45 7H50L43 31ZM43 5L38 9H21L18 5L10 29L5 33H0L7 9L19 0H45L43 5ZM24 13H35L29 31L26 27H15L21 9L24 13Z"
                        fill="#3b3b3cff"
                    />
                </svg>
            </div>

            <div>
                <ul id="navbar" className={clicked ? "#navbar active" : "navbar"}>
                    <li onClick={() => goTo("/home")}><h2>Início</h2></li>
                    <li onClick={() => goTo("/persons")}><h2>Pessoas</h2></li>
                    <li onClick={() => goTo("/login")}><h2>Sair</h2></li>
                </ul>
            </div>

            <div id="mobile" onClick={handleClick}>
                <i id="bar" 
                    className={clicked ? 'fas fa-times' : 'fas fa-bars'}></i>
            </div>
        </nav>
    );
}

export default Navbar;