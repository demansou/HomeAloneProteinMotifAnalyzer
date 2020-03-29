// src/components/Home.js

import React from 'react'
import { Col, Row } from 'react-bootstrap'
import HomeDataTable from './HomeDataTable'

const Home = () => {

    return (
        <>
            <Row>
                <Col>
                    <h2>Available Data Sets</h2>
                </Col>
            </Row>
            <Row>
                <Col>
                    <HomeDataTable/>
                </Col>
            </Row>
        </>
    )
}

export default Home