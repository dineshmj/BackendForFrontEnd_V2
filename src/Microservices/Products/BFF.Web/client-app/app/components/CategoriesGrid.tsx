'use client';

import React from 'react';

interface Category {
  id: number;
  name: string;
}

interface CategoriesGridProps {
  categories: Category[];
}

const CategoriesGrid: React.FC<CategoriesGridProps> = ({ categories }) => {
  if (!categories || categories.length === 0) {
    return <p>No items found.</p>;
  }

  return (
    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))', gap: '1.5rem', marginTop: '2rem' }}>
      {categories.map((c) => (
        <div key={c.id} style={{ border: '1px solid #ddd', borderRadius: '8px', padding: '1rem', boxShadow: '0 2px 4px rgba(0,0,0,0.1)' }}>
          <h3 style={{ fontSize: '1.25rem', marginBottom: '0.5rem' }}>{c.name}</h3>
          <p style={{ margin: '0.25rem 0' }}><strong>ID:</strong> {c.id}</p>
        </div>
      ))}
    </div>
  );
};

export default CategoriesGrid;