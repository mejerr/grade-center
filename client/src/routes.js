import React, { lazy, Suspense, useEffect } from 'react';
import { Route, Switch, Redirect, useHistory } from 'react-router-dom';
import './app/App.scss';
import './App.css';

import Spinner from './app/shared/Spinner';

const Dashboard = lazy(() => import('./app/dashboard/Dashboard'));
const Buttons = lazy(() => import('./app/basic-ui/Buttons'));
const Dropdowns = lazy(() => import('./app/basic-ui/Dropdowns'));
const Typography = lazy(() => import('./app/basic-ui/Typography'));
const BasicElements = lazy(() => import('./app/form-elements/BasicElements'));
const BasicTable = lazy(() => import('./app/tables/BasicTable'));
const Mdi = lazy(() => import('./app/icons/Mdi'));
const ChartJs = lazy(() => import('./app/charts/ChartJs'));
const Error404 = lazy(() => import('./app/error-pages/Error404'));
const Error500 = lazy(() => import('./app/error-pages/Error500'));
const Login = lazy(() => import('./app/user-pages/Login'));
const Register = lazy(() => import('./app/user-pages/Register'));

const Routes = (props) => {
  const { isAuthenticated } = props;
  const history = useHistory();

  useEffect(() => {
    const unlisten = history.listen(() => {
      window.scrollTo(0, 0);
    });

    return () => {
      unlisten();
    };
  }, [history]);

  let routes = (
    <Switch>
      <Route path="/dashboard" component={Dashboard}/>
      <Route path="/basic-ui/buttons" component={Buttons}/>
      <Route path="/basic-ui/dropdowns" component={Dropdowns}/>
      <Route path="/basic-ui/typography" component={Typography}/>
      <Route path="/form-Elements/basic-elements" component={BasicElements}/>
      <Route path="/tables/basic-table" component={BasicTable}/>
      <Route path="/icons/mdi" component={Mdi}/>
      <Route path="/charts/chart-js" component={ChartJs}/>
      <Route path="/user-pages/login-1" component={Login}/>
      <Route path="/user-pages/register-1" component={Register}/>
      <Route path="/error-pages/error-404" component={Error404}/>
      <Route path="/error-pages/error-500" component={Error500}/>
      <Route path="/" exact component={Dashboard}/>
      <Redirect to="/" />
    </Switch>
  );

  if (isAuthenticated) {
    routes = (
      <Switch>
        <Route path="/dashboard" component={Dashboard} />
        <Redirect to="/" />
      </Switch>
    );
  }

  return (
    <Suspense fallback={<Spinner/>}>
      {routes}
    </Suspense>
  );
};

Routes.defaultProps = {
  isAuthenticated: false
};

export default Routes;
