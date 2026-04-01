import './Footer.css';

export default function Footer() {
  return (
    <footer className="footer">
      <div className="container">
        <div className="footer-content">
          <div className="footer-section">
            <h3 className="footer-title">LetsMeets</h3>
            <p className="footer-text">Event management and registration platform</p>
          </div>
          
          <div className="footer-section">
            <h4 className="footer-heading">Quick Links</h4>
            <ul className="footer-links">
              <li><a href="/events">Browse Events</a></li>
              <li><a href="/about">About</a></li>
              <li><a href="/login">Partner Login</a></li>
            </ul>
          </div>
          
          <div className="footer-section">
            <h4 className="footer-heading">For Partners</h4>
            <ul className="footer-links">
              <li><a href="/partner/dashboard">Dashboard</a></li>
              <li><a href="/partner/events">Manage Events</a></li>
            </ul>
          </div>
        </div>
        
        <div className="footer-bottom">
          <p>&copy; {new Date().getFullYear()} LetsMeets. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
}
