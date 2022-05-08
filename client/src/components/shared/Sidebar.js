import React, { useEffect, useState } from 'react';
import { Link, withRouter } from 'react-router-dom';
import { Collapse, Dropdown } from 'react-bootstrap';

const Sidebar = (props) => {
  const [state, setState] = useState({});
  const { history } = props;
  const { location } = history;

  const isPathActive = (path) => {
    return location.pathname.startsWith(path);
  };

  const toggleMenuState = (menuState) => {
    if (state[ menuState ]) {
      setState({ ...state, [ menuState ] : false });
    } else if (Object.keys(state).length === 0) {
      setState({ ...state, [ menuState ] : true });
    } else {
      Object.keys(state).forEach(i => {
        setState({ ...state, [ i ]: false });
      });
      setState({ ...state, [ menuState ] : true });
    }
  };

  const onRouteChanged = () => {
    document.querySelector('#sidebar').classList.remove('active');
    Object.keys(state).forEach(i => {
      setState({ ...state, [ i ]: false });
    });

    const dropdownPaths = [
      { path:'/apps', state: 'appsMenuOpen' },
      { path:'/create', state: 'createMenuOpen' },
    ];

    dropdownPaths.forEach((obj => {
      if (isPathActive(obj.path)) {
        setState({ ...state, [ obj.state ] : true });
      }
    }));
  };

  useEffect(() => {
    onRouteChanged();
  }, [location]);

  useEffect(() => {
    onRouteChanged();

    const body = document.querySelector('body');
    document.querySelectorAll('.sidebar .nav-item').forEach((el) => {

      el.addEventListener('mouseover', function() {
        if (body.classList.contains('sidebar-icon-only')) {
          el.classList.add('hover-open');
        }
      });
      el.addEventListener('mouseout', function() {
        if (body.classList.contains('sidebar-icon-only')) {
          el.classList.remove('hover-open');
        }
      });
    });
  }, []);


  return (
    <nav className="sidebar sidebar-offcanvas" id="sidebar">
      <div className="sidebar-brand-wrapper d-none d-lg-flex align-items-center justify-content-center fixed-top">
        <a className="sidebar-brand brand-logo" href="index.html"><img src={require('../../assets/images/logo.svg')} alt="logo" /></a>
        <a className="sidebar-brand brand-logo-mini" href="index.html"><img src={require('../../assets/images/logo-mini.svg')} alt="logo" /></a>
      </div>
      <ul className="nav">
        <li className="nav-item profile">
          <div className="profile-desc">
            <div className="profile-pic">
              <div className="profile-name">
                <h5 className="mb-0 font-weight-normal">Henry Klein</h5>
                <span>Gold Member</span>
              </div>
            </div>
            <Dropdown alignRight>
              <Dropdown.Toggle as="a" className="cursor-pointer no-caret">
                <i className="mdi mdi-dots-vertical"></i>
              </Dropdown.Toggle>
              <Dropdown.Menu className="sidebar-dropdown preview-list">
                <a href="!#" className="dropdown-item preview-item" onClick={evt => evt.preventDefault()}>
                  <div className="preview-thumbnail">
                    <div className="preview-icon bg-dark rounded-circle">
                      <i className="mdi mdi-settings text-primary"></i>
                    </div>
                  </div>
                  <div className="preview-item-content">
                    <p className="preview-subject ellipsis mb-1 text-small">Account settings</p>
                  </div>
                </a>
                <div className="dropdown-divider"></div>
                <a href="!#" className="dropdown-item preview-item" onClick={evt => evt.preventDefault()}>
                  <div className="preview-thumbnail">
                    <div className="preview-icon bg-dark rounded-circle">
                      <i className="mdi mdi-onepassword  text-info"></i>
                    </div>
                  </div>
                  <div className="preview-item-content">
                    <p className="preview-subject ellipsis mb-1 text-small">Change Password</p>
                  </div>
                </a>
                <div className="dropdown-divider"></div>
                <a href="!#" className="dropdown-item preview-item" onClick={evt => evt.preventDefault()}>
                  <div className="preview-thumbnail">
                    <div className="preview-icon bg-dark rounded-circle">
                      <i className="mdi mdi-calendar-today text-success"></i>
                    </div>
                  </div>
                  <div className="preview-item-content">
                    <p className="preview-subject ellipsis mb-1 text-small">To-do list</p>
                  </div>
                </a>
              </Dropdown.Menu>
            </Dropdown>
          </div>
        </li>
        <li className="nav-item nav-category">
          <span className="nav-link">Navigation</span>
        </li>
        <li className={isPathActive('/dashboard') ? 'nav-item menu-items active' : 'nav-item menu-items'}>
          <Link className="nav-link" to="/dashboard">
            <span className="menu-icon"><i className="mdi mdi-speedometer"></i></span>
            <span className="menu-title">Dashboard</span>
          </Link>
        </li>
        <li className={isPathActive('/create') ? 'nav-item menu-items active' : 'nav-item menu-items'}>
          <div className={ state?.createMenuOpen ? 'nav-link menu-expanded' : 'nav-link'} onClick={ () => toggleMenuState('createMenuOpen')} data-toggle="collapse">
            <span className="menu-icon">
              <i className="mdi mdi-laptop"></i>
            </span>
            <span className="menu-title">Create</span>
            <i className="menu-arrow"></i>
          </div>
          <Collapse in={ state?.createMenuOpen }>
            <div>
              <ul className="nav flex-column sub-menu">
                <li className="nav-item"> <Link className={isPathActive('/create/register') ? 'nav-link active' : 'nav-link'} to="/create/register">Users</Link></li>
                <li className="nav-item"> <Link className={isPathActive('/create/schools') ? 'nav-link active' : 'nav-link'} to="/create/schools">Schools</Link></li>
              </ul>
            </div>
          </Collapse>
        </li>
      </ul>
    </nav>
  );
};

export default withRouter(Sidebar);
