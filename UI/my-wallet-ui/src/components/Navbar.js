import { Component } from "react";
import "./Navbar.css";

class Navbar extends Component {
    state = { clicked: false };

    handleClick = () => {
        this.setState({clicked: !this.state.clicked});
    }

    render() {

        return (
            <>
                <nav>
                    <a href="index.html">
                        <svg width="50"
                            height="40"
                            viewBox="0 0 50 40"
                            fill="none"
                            xmlns="http://www.w3.org/2000/svg"
                        >
                            <path d="M43 31L31 40H5L7 35L12 31H29L32 35L40 11L45 7H50L43 31ZM43 5L38 9H21L18 5L10 29L5 33H0L7 9L19 0H45L43 5ZM24 13H35L29 31L26 27H15L21 9L24 13Z"
                                fill="#3b3b3cff"></path>
                        </svg>
                    </a>

                    <div>
                        <ul id="navbar" className={this.state.clicked ? "#navbar active" : "navbar"}>
                            <li><a className="active" href="index.html">Início</a></li>
                            <li><a href="persons.html">Pessoas</a></li>
                            <li><a href="wallets.html">Carteiras</a></li>
                            <li><a href="debts.html">Contas</a></li>
                        </ul>
                    </div>

                    <div id="mobile" onClick={this.handleClick}>
                        <i id="bar" className={this.state.clicked ? 'fas fa-times' : 'fas fa-bars'}></i>
                    </div>
                </nav>
            </>
        )
    }
}

export default Navbar;