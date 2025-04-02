import { useState, useEffect, useContext } from 'react';
import { aboutService } from '../../services/api';
import { AuthContext } from '../../context/AuthContext';

const About = () => {
    const { user } = useContext(AuthContext);
    const [content, setContent] = useState('');
    const [lastUpdated, setLastUpdated] = useState('');
    const [lastUpdatedBy, setLastUpdatedBy] = useState('');

    useEffect(() => {
        fetchAboutContent();
    }, []);

    const fetchAboutContent = async () => {
        try {
            const data = await aboutService.getAboutContent();
            setContent(data.content);
            setLastUpdated(data.lastUpdatedDate);
            setLastUpdatedBy(data.lastUpdatedBy);
        } catch (error) {
            console.error('Error fetching about content:', error);
        }
    };

    const handleUpdate = async () => {
        if (!user?.isAdmin) {
            alert('Only admins can update content.');
            return;
        }
        const newContent = prompt('Enter new content:');
        if (newContent) {
            try {
                await aboutService.updateAboutContent(newContent);
                fetchAboutContent();
            } catch (error) {
                alert('Failed to update content: ' + error.response?.data?.message);
            }
        }
    };

    return (
        <div>
            <h1>About</h1>
            <p>{content}</p>
            <p>Last Updated: {lastUpdated}</p>
            <p>Last Updated By: {lastUpdatedBy}</p>
            {user?.isAdmin && <button onClick={handleUpdate}>Update Content</button>}
        </div>
    );
};

export default About;