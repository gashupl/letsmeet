import { ButtonHTMLAttributes, ReactNode } from 'react';
import './Button.css';

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  children: ReactNode;
  variant?: 'primary' | 'secondary' | 'outline' | 'ghost' | 'accent' | 'warning';
  size?: 'small' | 'medium' | 'large';
  fullWidth?: boolean;
}

export default function Button({ 
  children, 
  variant = 'primary', 
  size = 'medium',
  type = 'button',
  onClick,
  disabled = false,
  fullWidth = false,
  ...props 
}: ButtonProps) {
  const className = `btn btn-${variant} btn-${size} ${fullWidth ? 'btn-full' : ''}`.trim();
  
  return (
    <button 
      type={type}
      className={className}
      onClick={onClick}
      disabled={disabled}
      {...props}
    >
      {children}
    </button>
  );
}
