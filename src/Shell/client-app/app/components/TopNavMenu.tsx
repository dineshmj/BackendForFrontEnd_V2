'use client';

import React, { useRef, useState } from 'react';
import { Microservice, MenuItem } from '../types';
import styles from './TopNavMenu.module.css';

interface TopNavMenuProps {
  microservices: Microservice[];
  handleMenuItemClick: (item: MenuItem) => void;
  loading: boolean;
}

// How long the pointer must rest on a Management Area before its
// leaf-node menu items (Level 3) fly out. Keeps quick mouse passes
// from flashing every submenu open.
const AREA_HOVER_DELAY_MS = 350;

export const TopNavMenu: React.FC<TopNavMenuProps> = ({
  microservices,
  handleMenuItemClick,
  loading,
}) => {
  // Level 1: which Microservice's dropdown (Products / Orders) is open.
  const [openMicroservice, setOpenMicroservice] = useState<string | null>(null);
  // Level 2 -> 3: which Management Area's leaf-node flyout is open.
  const [openArea, setOpenArea] = useState<string | null>(null);

  const areaHoverTimer = useRef<ReturnType<typeof setTimeout> | null>(null);

  const clearAreaTimer = () => {
    if (areaHoverTimer.current) {
      clearTimeout(areaHoverTimer.current);
      areaHoverTimer.current = null;
    }
  };

  const closeAll = () => {
    setOpenMicroservice(null);
    setOpenArea(null);
    clearAreaTimer();
  };

  const handleAreaEnter = (areaName: string) => {
    clearAreaTimer();
    // Hide any previously open flyout immediately, then wait before
    // showing the new one — this is the "hover for some time" delay.
    setOpenArea(null);
    areaHoverTimer.current = setTimeout(() => {
      setOpenArea(areaName);
    }, AREA_HOVER_DELAY_MS);
  };

  const handleAreaLeave = () => {
    clearAreaTimer();
  };

  return (
    <nav className={styles.navBar} onMouseLeave={closeAll}>
      {microservices.map((microservice) => (
        <div
          key={microservice.name}
          className={styles.navItem}
          onMouseEnter={() => setOpenMicroservice(microservice.name)}
        >
          <button type="button" className={styles.navButton}>
            {microservice.name}
            <span className={styles.arrow}>&#9662;</span>
          </button>

          {openMicroservice === microservice.name && (
            <div className={styles.dropdown}>
              {microservice.managementAreas.map((area) => (
                <div
                  key={area.name}
                  className={styles.areaRow}
                  onMouseEnter={() => handleAreaEnter(area.name)}
                  onMouseLeave={handleAreaLeave}
                >
                  <span className={styles.areaLabel}>{area.name}</span>
                  <span className={styles.areaArrow}>&#9656;</span>

                  {openArea === area.name && (
                    <div className={styles.flyout}>
                      <ul className={styles.flyoutList}>
                        {area.menuItems.map((item) => (
                          <li key={item.taskName}>
                            <a
                              href="#"
                              className={
                                loading
                                  ? `${styles.leafLink} ${styles.leafLinkDisabled}`
                                  : styles.leafLink
                              }
                              onClick={(e) => {
                                e.preventDefault();
                                if (!loading) {
                                  handleMenuItemClick({
                                    ...item,
                                    managementAreaName: area.name,
                                    microserviceName: microservice.name,
                                    baseURL: microservice.baseURL,
                                  });
                                  closeAll();
                                }
                              }}
                            >
                              {item.taskName} {loading && '...'}
                            </a>
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}
                </div>
              ))}
            </div>
          )}
        </div>
      ))}
    </nav>
  );
};
