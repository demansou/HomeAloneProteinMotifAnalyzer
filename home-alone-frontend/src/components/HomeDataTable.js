// src/components/HomeDataTable.js

import React, { useEffect, useState} from 'react'
import { Link } from 'react-router-dom'
import { Table } from 'react-bootstrap'

const DataSetMapper = ({data}) => {
    const sequenceTypeMapper = (type) => {
        switch (type) {
            case 0:
                return 'DNA'
            case 1:
                return 'RNA'
            case 2:
                return 'Protein'
            default:
                return 'Data error...'
        }
    }

    return (
        <tr>
            <td>{data.name}</td>
            <td>{sequenceTypeMapper(data.sequenceType)}</td>
            <td>
                <Link to={`/analyze/${data.id}`}>
                    Analyze =>
                </Link>
            </td>
        </tr>
    )
}

const DataErrorRow = () => {
    return (
        <tr><td colSpan={3}>Unable to load data...</td></tr>
    )
}

const HomeDataTable = () => {
    const [dataSets, setDataSets] = useState(null)

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/DataSetModels`)
            .then(data => data.json())
            .then(json => setDataSets(json))
            .catch(err => {
                console.log(err)
                setDataSets(null)
            })
    }, [])

    return (
        <Table striped bordered hover>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Sequence Type</th>
                    <th>&nbsp;</th>
                </tr>
            </thead>
            <tbody>
                {
                    dataSets && dataSets.length > 0
                        ? dataSets.map((data, key) => <DataSetMapper data={data} key={key}/>)
                        : <DataErrorRow/>
                }
            </tbody>
        </Table>
    )
}

export default HomeDataTable