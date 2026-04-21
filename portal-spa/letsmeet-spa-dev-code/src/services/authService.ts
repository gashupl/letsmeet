export interface UserInfo {
  contactId: string;
  fullName: string;
  email: string;
}

interface LoginRequest {
  username: string;
  password: string;
}

interface AuthStatusResponse {
  isAuthenticated: boolean;
  user: UserInfo | null;
}

const AUTH_BASE = '/_api/auth';

function getRequestVerificationToken(): string {
  const tokenElement = document.querySelector<HTMLMetaElement>(
    'meta[name="__RequestVerificationToken"]'
  );
  if (tokenElement) {
    return tokenElement.content;
  }

  const cookieMatch = document.cookie
    .split('; ')
    .find((row) => row.startsWith('__RequestVerificationToken='));
  return cookieMatch ? decodeURIComponent(cookieMatch.split('=')[1]) : '';
}

export async function login(credentials: LoginRequest): Promise<UserInfo> {
  const response = await fetch(`${AUTH_BASE}/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      '__RequestVerificationToken': getRequestVerificationToken(),
    },
    credentials: 'include',
    body: JSON.stringify({
      username: credentials.username,
      password: credentials.password,
    }),
  });

  if (!response.ok) {
    const error = await response.json().catch(() => null);
    throw new Error(error?.message ?? 'Invalid username or password.');
  }

  return response.json();
}

export async function logout(): Promise<void> {
  const response = await fetch(`${AUTH_BASE}/logout`, {
    method: 'POST',
    headers: {
      '__RequestVerificationToken': getRequestVerificationToken(),
    },
    credentials: 'include',
  });

  if (!response.ok) {
    throw new Error('Logout failed.');
  }
}

export async function getUserInfo(): Promise<AuthStatusResponse> {
  try {
    const response = await fetch(`${AUTH_BASE}/getuserinfo`, {
      method: 'GET',
      credentials: 'include',
    });

    if (!response.ok) {
      return { isAuthenticated: false, user: null };
    }

    const user: UserInfo = await response.json();
    return { isAuthenticated: true, user };
  } catch {
    return { isAuthenticated: false, user: null };
  }
}
