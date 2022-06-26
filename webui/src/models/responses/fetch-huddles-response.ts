export interface FetchHuddlesResponse {
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
