import { Link, useNavigate } from 'react-router-dom';
import './Header.css';
import Button from './Button';
import { useAuth } from '../context/AuthContext';

export default function Header() {
  const { isAuthenticated, user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = async () => {
    await logout();
    navigate('/');
  };

  return (
    <header className="header">
      <div className="container">
        <div className="header-content">
          <Link to="/" className="logo">
            <span className="logo-icon">📅</span>
            <span className="logo-text">Let's Meet! (v.7)</span>
          </Link>
          
          <nav className="nav">
            <Link to="/events" className="nav-link">Browse Events</Link>
            <Link to="/about" className="nav-link">About</Link>
            {isAuthenticated ? (
              <div className="auth-info">
                <Link to="/partner/dashboard" className="nav-link">Dashboard</Link>
                <span className="welcome-text">Welcome {user?.fullName}</span>
                <Button variant="outline" size="small" onClick={handleLogout}>
                  Logout
                </Button>
              </div>
            ) : (
              <Link to="/login">
                <Button variant="outline" size="small">Partner Login</Button>
              </Link>
            )}
          </nav>
        </div>
      </div>
    </header>
  );
}
