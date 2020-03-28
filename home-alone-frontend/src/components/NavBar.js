// src/components/NavBar.js

import React from 'react'
import { Navbar } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import logo from '../logo.svg'

const NavBar = () => {
    return (
        <Navbar bg='dark'>
            <Navbar.Brand as={Link} to='/'>
                <img
                    src={logo}
                    height='30'
                    width='30'
                    className='d-inline-block align-top'
                    />
                Protein Motif Analyzer
            </Navbar.Brand>
        </Navbar>
    )
}

export default NavBar