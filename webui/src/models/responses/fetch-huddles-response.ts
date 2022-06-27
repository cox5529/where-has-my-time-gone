export interface FetchHuddlesResponse {
  start: string;
  profiles: FetchHuddlesProfileResponse[];
}

export interface FetchHuddlesProfileResponse {
  userProfileId: string;
  name: string;
  profileImage: string | null;
  huddles: Huddle[];
}

export interface Huddle {
  start: string;
  end: string;
  groups: string[];
}
