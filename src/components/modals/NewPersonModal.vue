<script setup lang="ts">
import { useModalStore } from '@/stores/modals';
import { usePersonStore } from '@/stores/people';
import { Form } from 'vee-validate';
import * as Yup from 'yup';

import ModalWrapper from './ModalWrapper.vue';

import TextButton from '../TextButton.vue';
import BaseInput from '../form-fields/BaseInput.vue';
import SectionHeader from '../typography/SectionHeader.vue';

const rules = Yup.string().required('This field is required');

const people = usePersonStore();
const modals = useModalStore();

function onSubmit(values: any): void {
  const name = values.name as string;
  people.addPerson(name);
  modals.hide('addPerson');
}
</script>

<template>
  <ModalWrapper name="addPerson">
    <SectionHeader>Add Person</SectionHeader>
    <Form @submit="onSubmit">
      <BaseInput label="Name" type="text" name="name" :rules="rules" />
      <TextButton class="mt-4" type="submit" color="primary">Submit</TextButton>
    </Form>
  </ModalWrapper>
</template>
