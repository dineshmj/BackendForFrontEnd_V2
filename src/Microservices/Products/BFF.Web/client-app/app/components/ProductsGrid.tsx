'use client';

import React from 'react';

interface Product {
  id: number;
  name: string;
  price: number;
  categoryName: string;
  stockQuantity: number;
}

interface ProductsGridProps {
  products: Product[];
}

const ProductsGrid: React.FC<ProductsGridProps> = ({ products }) => {
  if (!products || products.length === 0) {
    return <p>No items found.</p>;
  }

  return (
    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))', gap: '1.5rem', marginTop: '2rem' }}>
      {products.map((p) => (
        <div key={p.id} style={{ border: '1px solid #ddd', borderRadius: '8px', padding: '1rem', boxShadow: '0 2px 4px rgba(0,0,0,0.1)' }}>
          <h3 style={{ fontSize: '1.25rem', marginBottom: '0.5rem' }}>{p.name}</h3>
          <p style={{ margin: '0.25rem 0' }}><strong>Price:</strong> ${p.price.toFixed(2)}</p>
          <p style={{ margin: '0.25rem 0' }}><strong>Category:</strong> {p.categoryName}</p>
          <p style={{ margin: '0.25rem 0' }}><strong>Stock:</strong> {p.stockQuantity}</p>
        </div>
      ))}
    </div>
  );
};

export default ProductsGrid;