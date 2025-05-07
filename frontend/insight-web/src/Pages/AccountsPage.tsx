import { useEffect, useState } from 'react';
import { Account } from '../account';

export default function AccountsPage() {
  const [accounts, setAccounts] = useState<Account[]>([]);

  useEffect(() => {
    fetch('http://localhost:5020/cds-au/v1/banking/accounts')
      .then((res) => res.json())
      .then(setAccounts)
      .catch((err) => console.error('Failed to fetch accounts:', err));
  }, []);

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '1rem', padding: '1rem' }}>
    {accounts.map((account) => (
      <div
        key={account.accountId}
        style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          border: '1px solid #ccc',
          borderRadius: '8px',
          padding: '1rem',
          backgroundColor: '#f9f9f9',
          boxShadow: '0 2px 4px rgba(0,0,0,0.05)',
        }}
      >
        <div><strong>{account.displayName}</strong></div>
        <div>{account.nickname || '-'}</div>
        <div>{account.openStatus || 'OPEN'}</div>
        <div>{account.isOwned !== false ? 'Owned' : 'Not Owned'}</div>
      </div>
    ))}
  </div>
  );
}