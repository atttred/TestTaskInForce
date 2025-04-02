import { useState, useEffect, useContext } from 'react';
import { shortUrlService } from '../../services/api';
import { AuthContext } from '../../context/AuthContext';

const Urls = () => {
    const { user } = useContext(AuthContext);
    const [urls, setUrls] = useState([]);

    useEffect(() => {
        fetchUrls();
    }, [user]);

    const fetchUrls = async () => {
        try {
            const data = await shortUrlService.getAllUrls();
            setUrls(data);
        } catch (error) {
            console.error('Error fetching URLs:', error);
        }
    };

    const handleCreate = async () => {
        if (!user) {
            alert('Please log in to create a URL.');
            return;
        }
        const originalUrl = prompt('Enter original URL:');
        if (originalUrl) {
            try {
                await shortUrlService.createShortUrl(originalUrl);
                fetchUrls();
            } catch (error) {
                alert('Failed to create URL: ' + error.response?.data?.message);
            }
        }
    };

    const handleDelete = async (shortCode) => {
        if (!user) {
            alert('Please log in to delete a URL.');
            return;
        }
        if (window.confirm('Are you sure?')) {
            try {
                await shortUrlService.deleteUrl(shortCode);
                fetchUrls();
            } catch (error) {
                alert('Failed to delete URL: ' + error.response?.data?.message);
            }
        }
    };

    return (
        <div>
            <h1>Short URLs</h1>
            {user && <button onClick={handleCreate}>Create New URL</button>}
            <ul>
                {urls.map((url) => (
                    <li key={url.id}>
                        <a href={`/api/ShortUrl/redirect/${url.shortCode}`} target="_blank">{url.originalUrl}</a> ({url.shortCode})
                        {user && <button onClick={() => handleDelete(url.shortCode)}>Delete</button>}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Urls;