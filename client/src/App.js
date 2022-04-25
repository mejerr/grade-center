import  React from 'react';
import './theme-app/App.scss';
import './App.css';

import Footer from './theme-app/shared/Footer';
import Sidebar from './theme-app/shared/Sidebar';
import Navbar from './theme-app/shared/Navbar';
import Routes from './routes';

const App = () => {
  return (
    <div className="container-scroller">
      <Sidebar/>
      <div className="container-fluid page-body-wrapper">
        <Navbar/>
        <div className="main-panel">
          <div className="content-wrapper">
            <Routes />
          </div>
          <Footer/>
        </div>
      </div>
    </div>
  );
};

export default App;
