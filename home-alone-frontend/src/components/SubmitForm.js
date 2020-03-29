// src/components/SubmitForm.js

import React, { useReducer } from 'react'
import { Form, Row, Col, Button } from 'react-bootstrap'
import SubmitReducer from '../reducers/SubmitReducer'
import { UPDATE_COLLECTION_NAME, UPDATE_SEQUENCE_TYPE, UPDATE_FILE, UPDATE_FILE_LOCATION } from '../reducers/SubmitReducer'

const SubmitForm = () => {
    const [{ collectionName, sequenceType, file, fileLocation }, dispatch] =
        useReducer(SubmitReducer, { collectionName: '', sequenceType: 0, file: null, fileLocation: null })

    const onButtonClick = (e) => {
        e.preventDefault()

        if (!collectionName || !sequenceType || !file || !fileLocation) {
            return
        }

        const formData = new FormData()
        formData.append('File', file, fileLocation)
        formData.append('CollectionName', collectionName)
        formData.append('SequenceType', sequenceType)

        fetch(`${process.env.REACT_APP_API_URL}/api/Submit`, {
            method: 'POST',
            body: formData
        })
            .then(data => data.json())
            .then(json => console.log(json))
            .catch(err => console.log(err))
    }

    return (
        <Form>
            <Row>
                <Col>
                    <h2>Submit Fasta File</h2>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Form.Group controlId='collectionName'>
                        <Form.Label>Collection Name</Form.Label>
                        <Form.Control
                            value={collectionName}
                            onChange={e => dispatch({ type: UPDATE_COLLECTION_NAME, payload: e.target.value })}
                            />
                    </Form.Group>
                </Col>
                <Col>
                    <Form.Group controlId='sequenceType'>
                        <Form.Label>Sequence Type</Form.Label>
                        <Form.Control
                            as='select'
                            value={sequenceType}
                            onChange={e => dispatch({ type: UPDATE_SEQUENCE_TYPE, payload: e.target.value })}
                            >
                            <option value=''></option>
                            <option value={3}>Protein</option>
                            <option value={1} disabled>DNA</option>
                            <option value={2} disabled>RNA</option>
                        </Form.Control>
                    </Form.Group>
                </Col>
                <Col>
                    <Form.Group controlId='file'>
                        <Form.Label>File</Form.Label>
                        <Form.File onChange={e => {
                            dispatch({ type: UPDATE_FILE, payload: e.target.files[0] })
                            dispatch({ type: UPDATE_FILE_LOCATION, payload: e.target.value })
                        }}
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
                        disabled
                        >
                        Submit Form
                    </Button>
                </Col>
            </Row>
        </Form>
    )
}

export default SubmitForm