//src/components/AnalyzeFilter.js

import React, { useReducer, useContext } from 'react'
import { Form, Row, Col, Button } from 'react-bootstrap'
import AnalyzeFilterReducer from '../reducers/AnalyzeFilterReducer'
import { UPDATE_AASEARCH, UPDATE_RANGE, UPDATE_FREQUENCY } from '../reducers/AnalyzeFilterReducer'
import { AnalyzeContext } from '../contexts/AnalyzeContext'

const AnalyzeFilter = ({ id }) => {
    const [{ aaSearch, range, frequency }, dispatch] =
        useReducer(AnalyzeFilterReducer, { aaSearch: '', range: 0, frequency: 1 })
    const { setFilterResults } = useContext(AnalyzeContext)

    const onButtonClick = (e) => {
        e.preventDefault()

        if (!aaSearch || !range || !frequency) {
            return
        }

        fetch(`${process.env.REACT_APP_API_URL}/api/Filter`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                DataSetModelId: id,
                AaSearch: aaSearch,
                Range: range,
                Frequency: frequency
            })
        })
            .then(data => data.json())
            .then(json => setFilterResults(json))
            .catch(err => console.log(err))
    }

    return (
        <Form>
            <Row>
                <Col>
                    <Form.Group controlId='aaSearch'>
                        <Form.Label>Amino Acid Sequence</Form.Label>
                        <Form.Control
                            value={aaSearch}
                            onChange={e => dispatch({ type: UPDATE_AASEARCH, payload: e.target.value })}
                            placeholder='Amino Acids to look for...'
                            />
                    </Form.Group>
                </Col>
                <Col>
                    <Form.Group controlId='range'>
                        <Form.Label>Range</Form.Label>
                        <Form.Control
                            type='number'
                            min={0}
                            max={250}
                            value={range}
                            onChange={e => dispatch({ type: UPDATE_RANGE, payload: e.target.value })}
                            />
                    </Form.Group>
                </Col>
                <Col>
                    <Form.Group controlId='frequency'>
                        <Form.Label>Frequency</Form.Label>
                        <Form.Control
                            type='number'
                            min={1}
                            max={100}
                            value={frequency}
                            onChange={e => dispatch({ type: UPDATE_FREQUENCY, payload: e.target.value })}
                            />
                    </Form.Group>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Button
                        type='submit'
                        onClick={e => onButtonClick(e)}
                        block
                        >
                        Filter
                    </Button>
                </Col>
            </Row>
        </Form>
    )
}

export default AnalyzeFilter