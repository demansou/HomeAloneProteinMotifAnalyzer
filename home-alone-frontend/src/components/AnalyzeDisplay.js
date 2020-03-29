// src/components/AnalyzeDisplay.js

import React, { useContext } from 'react'
import { AnalyzeContext } from '../contexts/AnalyzeContext'
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table'

const AnalyzeDisplay = () => {
    const { filterResults } = useContext(AnalyzeContext)

    return (
        <BootstrapTable
            data={filterResults}
            pagination
            >
            <TableHeaderColumn
                dataField='name'
                width={1}
                isKey
                >
                Name
            </TableHeaderColumn>
            <TableHeaderColumn
                dataField='data'
                width={2}
                >
                Sequence
            </TableHeaderColumn>
        </BootstrapTable>
    )
}

export default AnalyzeDisplay