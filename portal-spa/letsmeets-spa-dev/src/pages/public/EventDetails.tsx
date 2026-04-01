import { useParams, Link } from 'react-router-dom';
import Button from '../../components/Button';
import './EventDetails.css';

interface EventDetailsType {
  id: string | undefined;
  title: string;
  description: string;
  fullDescription: string;
  date: string;
  time: string;
  location: string;
  category: string;
  capacity: number;
  attendees: number;
  price: string;
  organizer: string;
  image: string;
}

export default function EventDetails() {
  const { id } = useParams<{ id: string }>();

  const mockEvent: EventDetailsType = {
    id: id,
    title: 'Web Development Summit 2026',
    description: 'Join us for the premier web development conference of the year. Connect with industry leaders, learn about cutting-edge technologies, and network with fellow developers from around the world.',
    fullDescription: `This comprehensive summit brings together the brightest minds in web development for three days of intensive learning, networking, and innovation.

    What to expect:
    • Keynote presentations from industry leaders
    • Hands-on workshops covering React, Vue, Angular, and more
    • Networking sessions with speakers and attendees
    • Expo hall featuring the latest tools and technologies
    • Career fair with leading tech companies
    
    Perfect for developers of all skill levels, from beginners to seasoned professionals.`,
    date: '2026-05-15',
    time: '9:00 AM - 6:00 PM',
    location: 'Moscone Center, San Francisco, CA',
    category: 'Technology',
    capacity: 200,
    attendees: 145,
    price: 'Free',
    organizer: 'Tech Events Inc.',
    image: 'https://placehold.co/800x400/2563eb/ffffff?text=Web+Development+Summit+2026'
  };

  const formatDate = (dateString: string): string => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { 
      weekday: 'long',
      month: 'long', 
      day: 'numeric', 
      year: 'numeric' 
    });
  };

  const spotsLeft = mockEvent.capacity - mockEvent.attendees;
  const isAlmostFull = spotsLeft < 20;

  return (
    <div className="event-details-page">
      <div className="event-hero">
        <img src={mockEvent.image} alt={mockEvent.title} />
        <div className="event-hero-overlay">
          <div className="container">
            <span className="event-category">{mockEvent.category}</span>
            <h1>{mockEvent.title}</h1>
          </div>
        </div>
      </div>

      <div className="container">
        <div className="event-content">
          <div className="event-main">
            <div className="event-section">
              <h2>About This Event</h2>
              <p className="event-description">{mockEvent.description}</p>
              <div className="event-full-description">
                {mockEvent.fullDescription.split('\n').map((paragraph, index) => (
                  <p key={index}>{paragraph}</p>
                ))}
              </div>
            </div>

            <div className="event-section">
              <h2>Event Details</h2>
              <div className="event-details-list">
                <div className="detail-item">
                  <span className="detail-icon">📅</span>
                  <div>
                    <strong>Date</strong>
                    <p>{formatDate(mockEvent.date)}</p>
                  </div>
                </div>

                <div className="detail-item">
                  <span className="detail-icon">🕐</span>
                  <div>
                    <strong>Time</strong>
                    <p>{mockEvent.time}</p>
                  </div>
                </div>

                <div className="detail-item">
                  <span className="detail-icon">📍</span>
                  <div>
                    <strong>Location</strong>
                    <p>{mockEvent.location}</p>
                  </div>
                </div>

                <div className="detail-item">
                  <span className="detail-icon">👤</span>
                  <div>
                    <strong>Organizer</strong>
                    <p>{mockEvent.organizer}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div className="event-sidebar">
            <div className="registration-card">
              <div className="price-tag">
                <span className="price">{mockEvent.price}</span>
              </div>

              <div className="availability">
                <div className="capacity-bar">
                  <div 
                    className="capacity-fill" 
                    style={{ width: `${(mockEvent.attendees / mockEvent.capacity) * 100}%` }}
                  ></div>
                </div>
                <p className={`spots-left ${isAlmostFull ? 'warning' : ''}`}>
                  {spotsLeft} spot{spotsLeft !== 1 ? 's' : ''} remaining
                </p>
              </div>

              <Link to={`/events/${id}/register`}>
                <Button size="large" fullWidth>Register Now</Button>
              </Link>

              <p className="registration-note">Registration confirmation sent via email</p>
            </div>

            <div className="share-card">
              <h3>Share This Event</h3>
              <div className="share-buttons">
                <button className="share-btn">📧 Email</button>
                <button className="share-btn">🔗 Copy Link</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
