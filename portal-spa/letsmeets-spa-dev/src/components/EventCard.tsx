import { Link } from 'react-router-dom';
import Button from './Button';
import './EventCard.css';

export interface Event {
  id: number;
  title: string;
  description: string;
  date: string;
  location: string;
  category: string;
  capacity?: number;
  attendees?: number;
  image?: string;
}

interface EventCardProps {
  event: Event;
}

export default function EventCard({ event }: EventCardProps) {
  const formatDate = (dateString: string): string => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { 
      month: 'short', 
      day: 'numeric', 
      year: 'numeric' 
    });
  };

  return (
    <div className="event-card">
      <div className="event-card-image">
        <img src={event.image || 'https://placehold.co/400x200?text=Event'} alt={event.title} />
        <span className="event-card-category">{event.category}</span>
      </div>
      
      <div className="event-card-content">
        <h3 className="event-card-title">{event.title}</h3>
        <p className="event-card-description">{event.description}</p>
        
        <div className="event-card-details">
          <div className="event-card-detail">
            <span className="detail-icon">📅</span>
            <span>{formatDate(event.date)}</span>
          </div>
          
          <div className="event-card-detail">
            <span className="detail-icon">📍</span>
            <span>{event.location}</span>
          </div>
          
          {event.capacity && (
            <div className="event-card-detail">
              <span className="detail-icon">👥</span>
              <span>{event.attendees || 0} / {event.capacity}</span>
            </div>
          )}
        </div>
        
        <Link to={`/events/${event.id}`}>
          <Button fullWidth>View Details</Button>
        </Link>
      </div>
    </div>
  );
}
