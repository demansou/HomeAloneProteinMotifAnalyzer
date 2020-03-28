import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import DefaultLayout from './layouts/DefaultLayout'
import Home from './components/Home'
import SubmitForm from './components/SubmitForm'
import AnalyzeForm from './components/AnalyzeForm'

import './App.css';

const App = () => {
  return (
    <BrowserRouter>
      <DefaultLayout>
        <Switch>
          <Route exact path='/'>
            <Home/>
          </Route>
          <Route path='/submit'>
            <SubmitForm/>
          </Route>
          <Route path='/analyze'>
            <AnalyzeForm/>
          </Route>
        </Switch>
      </DefaultLayout>
    </BrowserRouter>
  )
}

export default App;
