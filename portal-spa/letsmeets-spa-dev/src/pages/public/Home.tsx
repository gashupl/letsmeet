import { Link } from 'react-router-dom';
import Button from '../../components/Button';
import './Home.css';

interface FeaturedEvent {
  id: number;
  title: string;
  date: string;
  location: string;
  category: string;
  image: string;
}

export default function Home() {
  const featuredEvents: FeaturedEvent[] = [
    {
      id: 1,
      title: 'Web Development Summit 2026',
      date: '2026-05-15',
      location: 'San Francisco, CA',
      category: 'Technology',
      image: 'https://placehold.co/400x200/2563eb/ffffff?text=Tech+Summit'
    },
    {
      id: 2,
      title: 'Digital Marketing Workshop',
      date: '2026-06-10',
      location: 'New York, NY',
      category: 'Marketing',
      image: 'https://placehold.co/400x200/8b5cf6/ffffff?text=Marketing'
    },
    {
      id: 3,
      title: 'Startup Networking Mixer',
      date: '2026-05-28',
      location: 'Austin, TX',
      category: 'Networking',
      image: 'https://placehold.co/400x200/10b981/ffffff?text=Networking'
    }
  ];

  return (
    <div className="home">
      <section className="hero">
        <div className="container">
          <div className="hero-content">
            <h1 className="hero-title">
              Discover & Manage Events with Ease
            </h1>
            <p className="hero-subtitle">
              Let's Meet! connects event organizers with attendees. Create, manage, and register for events all in one platform.
            </p>
            <div className="hero-actions">
              <Link to="/events">
                <Button size="large">Browse Events</Button>
              </Link>
              <Link to="/login">
                <Button variant="outline" size="large">Partner Login</Button>
              </Link>
            </div>
          </div>
        </div>
      </section>

      <section className="featured-events">
        <div className="container">
          <div className="section-header">
            <h2 className="section-title">Featured Events</h2>
            <Link to="/events">
              <Button variant="ghost">View All Events →</Button>
            </Link>
          </div>
          
          <div className="events-grid">
            {featuredEvents.map(event => (
              <div key={event.id} className="featured-event-card">
                <div className="event-image">
                  <img src={event.image} alt={event.title} />
                  <span className="event-category">{event.category}</span>
                </div>
                <div className="event-info">
                  <h3>{event.title}</h3>
                  <p className="event-meta">
                    <span>📅 {new Date(event.date).toLocaleDateString()}</span>
                    <span>📍 {event.location}</span>
                  </p>
                  <Link to={`/events/${event.id}`}>
                    <Button fullWidth>View Details</Button>
                  </Link>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      <section className="cta">
        <div className="container">
          <div className="cta-content">
            <h2>Ready to Host Your Event?</h2>
            <p>Join our partner program and start creating memorable events today.</p>
            <Link to="/login">
              <Button variant="accent" size="large">Get Started</Button>
            </Link>
          </div>
        </div>
      </section>
    </div>
  );
}
