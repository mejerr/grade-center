import  React from 'react';
import './theme-app/App.scss';
import './App.css';

import Footer from './theme-app/shared/Footer';
import Sidebar from './theme-app/shared/Sidebar';
import Navbar from './theme-app/shared/Navbar';
import Routes from './routes';

const App = (props) => {
  const { isAuthenticated } = props;

  return (
    <div className="container-scroller app-container">
      {isAuthenticated && <Sidebar/>}
      <div className="container-fluid page-body-wrapper">
        {isAuthenticated && <Navbar/>}
        <div className="main-panel">
          <div className="content-wrapper">
            <Routes />
          </div>
          {isAuthenticated && <Footer/>}
        </div>
      </div>
    </div>
  );
};

App.defaultProps = {
  isAuthenticated: true
};

export default App;
