import React, { lazy, Suspense, useEffect } from 'react';
import { Route, Switch, Redirect, useHistory } from 'react-router-dom';
import Spinner from './theme-app/shared/Spinner';
import './theme-app/App.scss';
import './App.css';


const Login = lazy(() => import('./components/auth/Login'));
const Register = lazy(() => import('./components/auth/Register'));

const Dashboard = lazy(() => import('./theme-app/dashboard/Dashboard'));
const Buttons = lazy(() => import('./theme-app/basic-ui/Buttons'));
const Dropdowns = lazy(() => import('./theme-app/basic-ui/Dropdowns'));
const Typography = lazy(() => import('./theme-app/basic-ui/Typography'));
const BasicElements = lazy(() => import('./theme-app/form-elements/BasicElements'));
const BasicTable = lazy(() => import('./theme-app/tables/BasicTable'));
const Mdi = lazy(() => import('./theme-app/icons/Mdi'));
const ChartJs = lazy(() => import('./theme-app/charts/ChartJs'));
const Error404 = lazy(() => import('./theme-app/error-pages/Error404'));
const Error500 = lazy(() => import('./theme-app/error-pages/Error500'));


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
      <Route path="/user/login" component={Login}/>
      <Route path="/" exact component={Login}/>
      <Redirect to="/user/login" />
    </Switch>
  );

  if (isAuthenticated) {
    routes = (
      <Switch>
        {/* Grade center app routes*/}
        <Route path="/create/register" component={Register}/>
        <Route path="/user/login" component={Login}/>

        {/* Themed app routes*/}
        <Route path="/dashboard" component={Dashboard}/>
        <Route path="/basic-ui/buttons" component={Buttons}/>
        <Route path="/basic-ui/dropdowns" component={Dropdowns}/>
        <Route path="/basic-ui/typography" component={Typography}/>
        <Route path="/form-Elements/basic-elements" component={BasicElements}/>
        <Route path="/tables/basic-table" component={BasicTable}/>
        <Route path="/icons/mdi" component={Mdi}/>
        <Route path="/charts/chart-js" component={ChartJs}/>
        <Route path="/user/login" component={Login}/>
        <Route path="/user/register" component={Register}/>
        <Route path="/error-pages/error-404" component={Error404}/>
        <Route path="/error-pages/error-500" component={Error500}/>
        <Route path="/" exact component={Dashboard}/>
        <Redirect to="/dashboard" />
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
  isAuthenticated: true
};

export default Routes;
