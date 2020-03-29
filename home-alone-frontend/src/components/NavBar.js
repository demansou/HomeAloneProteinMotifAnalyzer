// src/components/NavBar.js

import React from 'react'
import { Navbar, Nav } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import logo from '../logo.svg'

const NavBar = () => {
    return (
        <Navbar variant='dark' expand='lg' className='bg-primary'>
            <Navbar.Brand as={Link} to='/'>
                <img
                    src={logo}
                    height='30'
                    width='30'
                    className='d-inline-block align-top'
                    alt='react logo'
                    />
                Protein Motif Analyzer
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav"/>
            <Nav className='mr-auto'>
                <Nav.Link as={Link} to='/'>Home</Nav.Link>
                <Nav.Link as={Link} to='/submit'>Submit Data Set</Nav.Link>
            </Nav>
        </Navbar>
    )
}

export default NavBar