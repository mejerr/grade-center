import React, { useCallback, useRef } from 'react';

const Register = () =>  {
  const emailNode = useRef();
  const passwordNode = useRef();

  const onRegisterClick = useCallback(() => {
    if (emailNode.current && passwordNode.current) {
      // send request with email and password and other values if needed-> emailNode/password.current.value
    }
  }, []);

  return (
    <div>
      <div className="d-flex align-items-center auth px-0 h-100">
        <div className="row w-100 mx-0">
          <div className="col-lg-4 mx-auto">
            <div className="card text-left py-5 px-4 px-sm-5">
              <h4>Регистриране на потребител</h4>
              <form className="pt-3">
                <div className="form-group">
                  <input ref={emailNode} type="email" className="form-control form-control-lg" id="exampleInputEmail1" placeholder="Email" />
                </div>
                <div className="form-group">
                  <select className="form-control form-control-lg" id="exampleFormControlSelect2">
                    <option>Country</option>
                    <option>United States of America</option>
                    <option>United Kingdom</option>
                    <option>India</option>
                    <option>Germany</option>
                    <option>Argentina</option>
                  </select>
                </div>
                <div className="form-group">
                  <input ref={passwordNode} type="password" className="form-control form-control-lg" id="exampleInputPassword1" placeholder="Password" />
                </div>
                <div className="mb-4">
                  <div className="form-check">
                    <label className="form-check-label text-muted">
                      <input type="checkbox" className="form-check-input" />
                      <i className="input-helper"></i>
                      {'I agree to all Terms & Conditions'}
                    </label>
                  </div>
                </div>
                <div className="mt-3">
                  <div className="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" onClick={onRegisterClick}>SIGN UP</div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
