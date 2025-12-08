'use client';

import React from 'react';
import { Microservice, MenuItem } from '../types';

interface MicroserviceAccordionProps {
  microservice: Microservice;
  isExpanded: boolean;
  onToggle: () => void;
  handleMenuItemClick: (item: MenuItem) => void;
  loading: boolean;
}

export const MicroserviceAccordion: React.FC<MicroserviceAccordionProps> = ({
  microservice,
  isExpanded,
  onToggle,
  handleMenuItemClick,
  loading,
}) => {
  return (
    <div style={{ marginBottom: '1.5rem', border: '1px solid #ddd', borderRadius: '5px' }}>
      {/* Accordion Header (Level 1) */}
      <button
        onClick={onToggle}
        style={{
          width: '100%',
          textAlign: 'left',
          padding: '0.75rem 1rem',
          backgroundColor: isExpanded ? '#e0f2f1' : '#f8f8f8',
          border: 'none',
          borderBottom: isExpanded ? '1px solid #ddd' : 'none',
          cursor: 'pointer',
          fontSize: '1rem',
          fontWeight: 'bold',
          color: '#2563eb',
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
        }}
      >
        {microservice.name}
        <span style={{ fontSize: '0.8rem', transform: isExpanded ? 'rotate(180deg)' : 'rotate(0deg)', transition: 'transform 0.2s' }}>
          &#9660; {/* Down arrow */}
        </span>
      </button>

      {/* Accordion Content (Level 2 & 3) */}
      {isExpanded && (
        <div style={{ padding: '1rem' }}>
          {microservice.managementAreas.map((area) => (
            <div key={area.name} style={{ marginTop: '0.5rem' }}>
              {/* Level 2: Management Area */}
              <h4 style={{ margin: '0.5rem 0', fontSize: '1rem', color: '#4b5563' }}>{area.name}</h4>
              <ul style={{ listStyle: 'none', paddingLeft: '1rem', margin: 0 }}>
                {area.menuItems.map((item) => (
                  <li key={item.taskName} style={{ marginBottom: '0.5rem' }}>
                    {/* Level 3: Menu Item Link */}
                    <a
                      href="#"
                      onClick={(e) => {
                        e.preventDefault();
                        if (!loading) {
                          handleMenuItemClick({
                            ...item,
                            managementAreaName: area.name,
                            microserviceName: microservice.name,
                            baseURL: microservice.baseURL,
                          });
                        }
                      }}
                      style={{
                        color: loading ? '#ccc' : '#10b981',
                        textDecoration: 'none',
                        cursor: loading ? 'not-allowed' : 'pointer',
                        fontWeight: '500',
                      }}
                    >
                      {item.taskName} {loading && '...'}
                    </a>
                  </li>
                ))}
              </ul>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};