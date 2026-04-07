import Button from '../../components/Button';
import './About.css';

export default function About() {
  return (
    <div className="about-page">
      <div className="container">
        <div className="about-hero">
          <h1>About Let's Meet!</h1>
          <p className="lead">Connecting event organizers with attendees worldwide</p>
        </div>

        <div className="about-content">
          <section className="about-section">
            <h2>Our Mission</h2>
            <p>
              Let's Meet! is dedicated to making event management and registration simple, efficient, 
              and accessible for everyone. We believe that great events bring people together and 
              create meaningful connections that last beyond the event itself.
            </p>
          </section>

          <section className="about-section">
            <h2>For Attendees</h2>
            <p>
              Discover events that match your interests, register with ease, and receive instant 
              confirmation. Our platform makes it simple to find and attend events that matter to you.
            </p>
            <ul className="features-list">
              <li>✓ Easy event discovery and search</li>
              <li>✓ Simple registration process</li>
              <li>✓ Instant email confirmations</li>
              <li>✓ Event reminders and updates</li>
            </ul>
          </section>

          <section className="about-section">
            <h2>For Partners</h2>
            <p>
              Create and manage events effortlessly with our powerful partner dashboard. Track 
              registrations, manage attendees, and gain insights into your event's performance.
            </p>
            <ul className="features-list">
              <li>✓ Intuitive event creation tools</li>
              <li>✓ Real-time registration tracking</li>
              <li>✓ Attendee management dashboard</li>
              <li>✓ Analytics and reporting</li>
            </ul>
          </section>

          <section className="about-section cta-section">
            <h2>Ready to Get Started?</h2>
            <p>Join thousands of event organizers and attendees using Let's Meet!</p>
            <div className="cta-buttons">
              <a href="/events"><Button size="large">Browse Events</Button></a>
              <a href="/login"><Button variant="outline" size="large">Become a Partner</Button></a>
            </div>
          </section>
        </div>
      </div>
    </div>
  );
}
