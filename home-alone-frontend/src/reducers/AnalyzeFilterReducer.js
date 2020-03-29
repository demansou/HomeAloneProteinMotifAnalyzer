// src/reducers/AnalyzeFilterReducer.js

export const UPDATE_AASEARCH = 'update-aasearch-val'
export const UPDATE_RANGE = 'update-range-val'
export const UPDATE_FREQUENCY = 'update-frequency-val'

const AnalyzeFilterReducer = (state, action) => {
    switch (action.type) {
        case UPDATE_AASEARCH:
            return { ...state, aaSearch: action.payload }
        case UPDATE_RANGE:
            return { ...state, range: action.payload }
        case UPDATE_FREQUENCY:
            return { ...state, frequency: action.payload }
        default:
            return state
    }
}

export default AnalyzeFilterReducer