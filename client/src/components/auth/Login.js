import React, { useCallback, useRef } from 'react';
import { Form } from 'react-bootstrap';

const Login = () => {
  const emailNode = useRef();
  const passwordNode = useRef();

  const onLoginClick = useCallback(() => {
    if (emailNode.current && passwordNode.current) {
      // send request with email and password -> emailNode/password.current.value
    }
  }, []);

  return (
    <div>
      <div className="d-flex align-items-center auth px-0">
        <div className="row w-100 mx-0">
          <div className="col-lg-4 mx-auto">
            <div className="card text-left py-5 px-4 px-sm-5">
              <div className="brand-logo">
                <img src={require('../../assets/images/logo.svg')} alt="logo" />
              </div>
              <h4>Здравейте!</h4>
              <h6 className="font-weight-light">Влезте в системата за да продължите.</h6>
              <Form className="pt-3">
                <Form.Group className="d-flex search-field">
                  <Form.Control ref={emailNode} type="email" placeholder="Username" size="lg" className="h-auto" />
                </Form.Group>
                <Form.Group className="d-flex search-field">
                  <Form.Control ref={passwordNode} type="password" placeholder="Password" size="lg" className="h-auto" />
                </Form.Group>
                <div className="mt-3">
                  <div className="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" onClick={onLoginClick}>SIGN IN</div>
                </div>
                <div className="my-2 d-flex justify-content-between align-items-center">
                  <div className="form-check">
                    <label className="form-check-label text-muted">
                      <input type="checkbox" className="form-check-input"/>
                      <i className="input-helper"></i>
                        Keep me signed in
                    </label>
                  </div>
                  <a href="!#" onClick={event => event.preventDefault()} className="auth-link text-muted">Forgot password?</a>
                </div>
              </Form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
