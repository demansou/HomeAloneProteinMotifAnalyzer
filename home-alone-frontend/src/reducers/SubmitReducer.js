// src/reducers/SubmitReducer.js

export const UPDATE_COLLECTION_NAME = 'update-collection-name'
export const UPDATE_SEQUENCE_TYPE = 'update-sequence-type'
export const UPDATE_FILE = 'update-file'
export const UPDATE_FILE_LOCATION = 'update-file-location'

const SubmitReducer = (state, action) => {
    switch (action.type) {
        case UPDATE_COLLECTION_NAME:
            return { ...state, collectionName: action.payload }
        case UPDATE_SEQUENCE_TYPE:
            return { ...state, sequenceType: action.payload }
        case UPDATE_FILE:
            return { ...state, file: action.payload }
        case UPDATE_FILE_LOCATION:
            return { ...state, fileLocation: action.payload }
        default:
            return state
    }
}

export default SubmitReducer