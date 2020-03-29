// src/components/AnalyzeDisplay.js

import React, { useContext } from 'react'
import { Table } from 'react-bootstrap'
import { AnalyzeContext } from '../contexts/AnalyzeContext'

const AnalyzeMapper = ({data}) => {
    return (
        <tr>
            <td>{data.name}</td>
            <td>{data.data}</td>
        </tr>
    )
}

const AnalyzeErrorRow = () => {
    return (
        <tr><td colSpan={2}>Unable to load data...</td></tr>
    )
}

const AnalyzeDisplay = () => {
    const { filterResults } = useContext(AnalyzeContext)

    return (
        <Table>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Sequence</th>
                </tr>
            </thead>
            <tbody>
                {
                    filterResults
                        ? filterResults.map((data, key) => <AnalyzeMapper data={data} key={key}/>)
                        : <AnalyzeErrorRow/>
                }
            </tbody>
        </Table>
    )
}

export default AnalyzeDisplay