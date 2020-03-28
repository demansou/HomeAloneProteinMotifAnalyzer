// src/components/HomeDataTable.js

import React, { useEffect, useState} from 'react'
import { Link } from 'react-router-dom'
import { Table } from 'react-bootstrap'

const DataSetMapper = ({data}) => {
    return (
        <tr>
            <td>{data.Name}</td>
            <td>{data.SequenceType}</td>
            <td>
                <Link to={`/analyze/${data.Id}`}>
                    Analyze =>
                </Link>
            </td>
        </tr>)
}

const DataErrorRow = () => {
    return (
        <tr><td colSpan={3}>Unable to load data...</td></tr>
    )
}

const HomeDataTable = () => {
    const [dataSets, setDataSets] = useState(null)

    useEffect(() => {
        fetch(`${process.env.REACT_APP_API_URL}/api/DataSetModel`)
            .then(data => data.json())
            .then(json => setDataSets(json.data))
            .catch(err => {
                console.log(err)
                setDataSets(null)
                // setDataSets([
                //     {Name: 'Test Name 1', SequenceType: 'DNA', Id: '1'},
                //     {Name: 'Test Name 2', SequenceType: 'RNA', Id: '2'},
                //     {Name: 'Jeanne Manalo', SequenceType: 'Grad Student', Id: '1992'}
                // ])
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