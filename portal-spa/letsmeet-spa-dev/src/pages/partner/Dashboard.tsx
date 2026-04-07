import { Link } from 'react-router-dom';
import Button from '../../components/Button';
import './Dashboard.css';

interface Stat {
  label: string;
  value: string;
  icon: string;
  color: 'primary' | 'accent' | 'secondary' | 'warning';
}

interface RecentEvent {
  id: number;
  title: string;
  date: string;
  registrations: number;
}

export default function Dashboard() {
  const stats: Stat[] = [
    { label: 'Total Events', value: '12', icon: '📅', color: 'primary' },
    { label: 'Total Registrations', value: '456', icon: '👥', color: 'accent' },
    { label: 'Upcoming Events', value: '5', icon: '⏰', color: 'secondary' },
    { label: 'This Month', value: '89', icon: '📊', color: 'warning' }
  ];

  const recentEvents: RecentEvent[] = [
    { id: 1, title: 'Web Development Summit', date: '2026-05-15', registrations: 145 },
    { id: 2, title: 'Digital Marketing Workshop', date: '2026-06-10', registrations: 67 },
    { id: 3, title: 'Startup Networking Mixer', date: '2026-05-28', registrations: 89 }
  ];

  return (
    <div className="dashboard-page">
      <div className="container">
        <div className="dashboard-header">
          <div>
            <h1>Partner Dashboard</h1>
            <p>Welcome back! Here's what's happening with your events.</p>
          </div>
          <Link to="/partner/events/new">
            <Button size="large">Create New Event</Button>
          </Link>
        </div>

        <div className="stats-grid">
          {stats.map((stat, index) => (
            <div key={index} className={`stat-card stat-${stat.color}`}>
              <div className="stat-icon">{stat.icon}</div>
              <div className="stat-content">
                <p className="stat-label">{stat.label}</p>
                <p className="stat-value">{stat.value}</p>
              </div>
            </div>
          ))}
        </div>

        <div className="dashboard-content">
          <div className="dashboard-section">
            <div className="section-header">
              <h2>Recent Events</h2>
              <Link to="/partner/events">
                <Button variant="ghost" size="small">View All</Button>
              </Link>
            </div>
            
            <div className="events-table">
              <table>
                <thead>
                  <tr>
                    <th>Event Name</th>
                    <th>Date</th>
                    <th>Registrations</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {recentEvents.map(event => (
                    <tr key={event.id}>
                      <td className="event-name">{event.title}</td>
                      <td>{new Date(event.date).toLocaleDateString()}</td>
                      <td><span className="badge">{event.registrations}</span></td>
                      <td>
                        <Link to={`/partner/events/${event.id}/edit`}>
                          <Button variant="ghost" size="small">Edit</Button>
                        </Link>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>

          <div className="dashboard-section">
            <h2>Quick Actions</h2>
            <div className="quick-actions">
              <Link to="/partner/events/new" className="action-card">
                <span className="action-icon">➕</span>
                <span className="action-label">Create Event</span>
              </Link>
              <Link to="/partner/events" className="action-card">
                <span className="action-icon">📋</span>
                <span className="action-label">Manage Events</span>
              </Link>
              <Link to="/partner/registrations" className="action-card">
                <span className="action-icon">👥</span>
                <span className="action-label">View Registrations</span>
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
