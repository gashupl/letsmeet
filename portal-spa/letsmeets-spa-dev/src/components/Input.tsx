import { InputHTMLAttributes } from 'react';
import './Input.css';

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  error?: string;
}

export default function Input({ 
  label, 
  error, 
  type = 'text',
  required = false,
  ...props 
}: InputProps) {
  return (
    <div className="input-group">
      {label && (
        <label className="input-label">
          {label}
          {required && <span className="input-required">*</span>}
        </label>
      )}
      <input 
        type={type}
        className={`input ${error ? 'input-error' : ''}`}
        required={required}
        {...props}
      />
      {error && <span className="input-error-message">{error}</span>}
    </div>
  );
}
