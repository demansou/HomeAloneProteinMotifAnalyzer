// src/contexts/AnalyzeContext.js

import React, { createContext, useState, useEffect } from 'react'

export const AnalyzeContext = createContext()

const AnalyzeContextProvider = (props) => {
    const [filterResults, setFilterResults] = useState(null)

    useEffect(() => {
        console.log(filterResults)
    }, [filterResults])
    return (
        <AnalyzeContext.Provider value={{filterResults, setFilterResults}}>
            {props.children}
        </AnalyzeContext.Provider>
    )
}

export default AnalyzeContextProvider