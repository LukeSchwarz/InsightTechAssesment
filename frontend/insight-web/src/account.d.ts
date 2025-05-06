export interface Account {
    accountId: string;
    displayName: string;
    nickname?: string;
    openStatus?: 'OPEN' | 'CLOSED';
    isOwned?: boolean;
}