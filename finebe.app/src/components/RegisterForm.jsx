import React, { useState } from 'react';
import {
  SlInput,
  SlButton,
  SlAlert,
} from '@shoelace-style/shoelace/dist/react';

function RegisterForm() {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
  });
  const [formError, setFormError] = useState(null);
  const [formSuccess, setFormSuccess] = useState(null);

  const handleChange = (e) => {
    const { id, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [id]: value,
    }));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (formData.password !== formData.confirmPassword) {
      setFormError('Passwords do not match!');
      setFormSuccess(null);
    } else {
      setFormError(null);
      setFormSuccess('Registration successful!');

      console.log('Registration successful!', formData);
      
      setFormData({
        username: '',
        email: '',
        password: '',
        confirmPassword: '',
      });
    }
  };

  return (
    <div style={{ maxWidth: '400px', margin: 'auto', padding: '1rem' }}>
      {formError && (
        <SlAlert variant="danger" closable>
          <strong>Error:</strong> {formError}
        </SlAlert>
      )}
      {formSuccess && (
        <SlAlert variant="success" closable>
          {formSuccess}
        </SlAlert>
      )}
      <form onSubmit={handleSubmit}>
        <SlInput
          label="Username"
          id="username"
          required
          value={formData.username}
          onSlInput={handleChange}
          style={{ marginBottom: '1rem' }}
        />
        <SlInput
          type="email"
          label="Email"
          id="email"
          required
          value={formData.email}
          onSlInput={handleChange}
          style={{ marginBottom: '1rem' }}
        />
        <SlInput
          type="password"
          label="Password"
          id="password"
          required
          value={formData.password}
          onSlInput={handleChange}
          style={{ marginBottom: '1rem' }}
        />
        <SlInput
          type="password"
          label="Confirm Password"
          id="confirmPassword"
          required
          value={formData.confirmPassword}
          onSlInput={handleChange}
          style={{ marginBottom: '1rem' }}
        />
        <SlButton type="submit" variant="primary" style={{ width: '100%' }}>
          Register
        </SlButton>
      </form>
    </div>
  );
}

export default RegisterForm;
