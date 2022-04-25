import  React, { Fragment, useState } from 'react';
import './theme-app/App.scss';
import './App.css';
import Footer from './theme-app/shared/Footer';
import ThemedSidebar from './theme-app/shared/Sidebar';
import Navbar from './theme-app/shared/Navbar';
import Routes from './routes';
import classnames from 'classnames';
import Sidebar from './components/shared/Sidebar';

const App = (props) => {
  const [openApp, setOpenApp] = useState(false);
  const { isAuthenticated } = props;
  let app;

  if (openApp) {
    app = (
      <Fragment>
        {isAuthenticated && <Sidebar/>}
        <div className="main-panel">
          <div className="content-wrapper">
            <Routes />
          </div>
          {isAuthenticated && <Footer/>}
        </div>
      </Fragment>
    );
  } else {
    app = (
      <Fragment>
        {isAuthenticated && <ThemedSidebar/>}
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
    <div className={classnames('container-scroller app-container', { 'opened': openApp })}>
      <div className="change-app-btn" onClick={() => setOpenApp(!openApp)}>Change app</div>
      {app}
    </div>
  );
};

App.defaultProps = {
  isAuthenticated: true
};

export default App;
