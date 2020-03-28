// src/layouts/DefaultLayout.js

import React from 'react'
import { Container, Row } from 'react-bootstrap'
import NavBar from '../components/NavBar'

const DefaultLayout = (props) => {
    const headerSeparator = {
        height: '30px'
    }

    return (
        <>
            <NavBar/>
            <Container>
                <Row style={headerSeparator}></Row>
                {props.children}
            </Container>
        </>
    )
}

export default DefaultLayout