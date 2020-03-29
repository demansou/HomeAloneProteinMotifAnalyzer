// src/components/AnalyzeForm.js

import React, { useEffect, useState } from 'react'
import { Row, Col } from 'react-bootstrap'
import { useParams } from 'react-router-dom'
import AnalyzeFilter from './AnalyzeFilter'
import AnalyzeDisplay from './AnalyzeDisplay'
import AnalyzeContextProvider from '../contexts/AnalyzeContext'

const AnalyzeForm = () => {
    const { id } = useParams()
    const [dataSet, setDataSet] = useState(null)

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/DataSetModels/${id}`)
            .then(data => data.json())
            .then(json => setDataSet(json))
            .catch(err => {
                console.log(err)
                setDataSet(null)
            })
    }, [id])

    return (
        <AnalyzeContextProvider>
            <Row>
                <Col>
                    <h2>{dataSet && dataSet.name ? `Analyze ${dataSet.name}` : 'Error loading data set...'}</h2>
                </Col>
            </Row>
            <Row>
                <Col>
                    <AnalyzeFilter id={id}/>
                </Col>
            </Row>
            <Row style={{ marginTop: 30 }}>
                <Col>
                    <AnalyzeDisplay/>
                </Col>
            </Row>
        </AnalyzeContextProvider>
    )
}

export default AnalyzeForm