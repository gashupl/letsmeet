import { Link } from 'react-router-dom';
import './Header.css';
import Button from './Button';

export default function Header() {
  return (
    <header className="header">
      <div className="container">
        <div className="header-content">
          <Link to="/" className="logo">
            <span className="logo-icon">📅</span>
            <span className="logo-text">Let's Meet!</span>
          </Link>
          
          <nav className="nav">
            <Link to="/events" className="nav-link">Browse Events</Link>
            <Link to="/about" className="nav-link">About</Link>
            <Link to="/login">
              <Button variant="outline" size="small">Partner Login</Button>
            </Link>
          </nav>
        </div>
      </div>
    </header>
  );
}
