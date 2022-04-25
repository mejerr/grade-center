import  React, { Fragment, useState } from 'react';
import './theme-app/App.scss';
import './App.css';

import Footer from './theme-app/shared/Footer';
import Sidebar from './theme-app/shared/Sidebar';
import Navbar from './theme-app/shared/Navbar';
import Routes from './routes';

const App = (props) => {
  const [openApp, setOpenApp] = useState(false);
  const { isAuthenticated } = props;
  let app;

  if (openApp) {
    app = (
      <Fragment>
        <Routes />
      </Fragment>
    );
  } else {
    app = (
      <Fragment>
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
      </Fragment>
    );
  }

  return (
    <div className="container-scroller app-container">
      <div className="change-app-btn" onClick={() => setOpenApp(!openApp)}>Change app</div>
      {app}
    </div>
  );
};

App.defaultProps = {
  isAuthenticated: true
};

export default App;
