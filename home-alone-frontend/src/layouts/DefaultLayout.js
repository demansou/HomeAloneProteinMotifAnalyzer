// src/layouts/DefaultLayout.js

import React from 'react'
import { Container, Column, Row } from 'react-bootstrap'
import NavBar from '../components/NavBar'

const DefaultLayout = (props) => {
    return (
        <Container fluid>
            <NavBar/>
            <Row>
                {props.children}
            </Row>
        </Container>
    )
}