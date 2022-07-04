<script setup lang="ts">
import AppTextButton from '../buttons/AppTextButton.vue';
import BaseInput from '../form-fields/BaseInput.vue';
import SectionHeader from '../typography/AppSectionHeader.vue';
import AppModal from './AppModal.vue';

import { Form } from 'vee-validate';
import * as Yup from 'yup';

import { useModalStore } from '@/stores/modals';
import { usePersonStore } from '@/stores/people';

const rules = Yup.string().required('This field is required');

const people = usePersonStore();
const modals = useModalStore();

const onSubmit: (values: any) => void = (values: { name: string }): void => {
  const name = values.name as string;
  people.addPerson(name);
  modals.hide('addPerson');
};
</script>

<template>
  <AppModal name="addPerson">
    <SectionHeader>Add Person</SectionHeader>
    <Form @submit="onSubmit">
      <BaseInput label="Name" type="text" name="name" :rules="rules" />
      <AppTextButton class="mt-4" type="submit" color="primary"
        >Submit</AppTextButton
      >
    </Form>
  </AppModal>
</template>
