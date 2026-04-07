import { useState } from 'react';
import EventCard from '../../components/EventCard';
import Input from '../../components/Input';
import { Event } from '../../components/EventCard';
import './Events.css';

export default function Events() {
  const [searchTerm, setSearchTerm] = useState<string>('');
  const [selectedCategory, setSelectedCategory] = useState<string>('all');

  const mockEvents: Event[] = [
    {
      id: 1,
      title: 'Web Development Summit 2026',
      description: 'Join industry leaders for a deep dive into modern web technologies and best practices.',
      date: '2026-05-15',
      location: 'San Francisco, CA',
      category: 'Technology',
      capacity: 200,
      attendees: 145,
      image: 'https://placehold.co/400x200/2563eb/ffffff?text=Tech+Summit'
    },
    {
      id: 2,
      title: 'Digital Marketing Workshop',
      description: 'Learn cutting-edge digital marketing strategies from experts in the field.',
      date: '2026-06-10',
      location: 'New York, NY',
      category: 'Marketing',
      capacity: 100,
      attendees: 67,
      image: 'https://placehold.co/400x200/8b5cf6/ffffff?text=Marketing'
    },
    {
      id: 3,
      title: 'Startup Networking Mixer',
      description: 'Connect with fellow entrepreneurs, investors, and innovators.',
      date: '2026-05-28',
      location: 'Austin, TX',
      category: 'Networking',
      capacity: 150,
      attendees: 89,
      image: 'https://placehold.co/400x200/10b981/ffffff?text=Networking'
    },
    {
      id: 4,
      title: 'UX Design Conference',
      description: 'Explore the latest trends in user experience and interface design.',
      date: '2026-07-05',
      location: 'Seattle, WA',
      category: 'Design',
      capacity: 180,
      attendees: 120,
      image: 'https://placehold.co/400x200/f59e0b/ffffff?text=UX+Design'
    },
    {
      id: 5,
      title: 'AI & Machine Learning Symposium',
      description: 'Discover the future of artificial intelligence and its applications.',
      date: '2026-08-20',
      location: 'Boston, MA',
      category: 'Technology',
      capacity: 250,
      attendees: 198,
      image: 'https://placehold.co/400x200/2563eb/ffffff?text=AI+Summit'
    },
    {
      id: 6,
      title: 'Business Leadership Forum',
      description: 'Develop your leadership skills with renowned business executives.',
      date: '2026-09-15',
      location: 'Chicago, IL',
      category: 'Business',
      capacity: 120,
      attendees: 45,
      image: 'https://placehold.co/400x200/ef4444/ffffff?text=Leadership'
    }
  ];

  const categories: string[] = ['all', 'Technology', 'Marketing', 'Networking', 'Design', 'Business'];

  const filteredEvents = mockEvents.filter(event => {
    const matchesSearch = event.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         event.description.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesCategory = selectedCategory === 'all' || event.category === selectedCategory;
    return matchesSearch && matchesCategory;
  });

  return (
    <div className="events-page">
      <div className="container">
        <div className="events-header">
          <h1>Discover Events</h1>
          <p>Find and register for events that interest you</p>
        </div>

        <div className="events-filters">
          <div className="search-bar">
            <Input
              type="search"
              placeholder="Search events..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>

          <div className="category-filters">
            {categories.map(category => (
              <button
                key={category}
                className={`category-btn ${selectedCategory === category ? 'active' : ''}`}
                onClick={() => setSelectedCategory(category)}
              >
                {category}
              </button>
            ))}
          </div>
        </div>

        <div className="events-results">
          <p className="results-count">
            {filteredEvents.length} event{filteredEvents.length !== 1 ? 's' : ''} found
          </p>

          <div className="events-grid">
            {filteredEvents.map(event => (
              <EventCard key={event.id} event={event} />
            ))}
          </div>

          {filteredEvents.length === 0 && (
            <div className="no-results">
              <p>No events found matching your criteria.</p>
              <p>Try adjusting your search or filters.</p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
