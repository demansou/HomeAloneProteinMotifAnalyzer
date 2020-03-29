// src/components/AnalyzeForm.js

import React, { useEffect, useState } from 'react'
import { Row, Col } from 'react-bootstrap'
import { useParams } from 'react-router-dom'

const AnalyzeForm = () => {
    const { dataSetId } = useParams()
    const [dataSet, setDataSet] = useState(null)

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/DataSetModels/${dataSetId}`)
            .then(data => data.json())
            .then(json => setDataSet(json))
            .catch(err => {
                console.log(err)
                setDataSet(null)
            })
    }, [dataSetId])

    return (
        <>
            <Row>
                <Col>
                    <h2>{dataSet && dataSet.Name ? `Analyze ${dataSet.Name}` : 'Error loading data set...'}</h2>
                </Col>
            </Row>
            <Row>
                <Col>
                    {/*TODO DM 03/28/2020 Get the filter component done.*/}
                </Col>
            </Row>
        </>
    )
}

export default AnalyzeForm